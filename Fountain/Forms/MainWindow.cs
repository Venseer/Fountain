/* Fountain - Map painting/generating software for worldbuilders. Copyright (C) 2016 Evan Llewellyn Price
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Fountain.Controls;
using Fountain.Media;

using LlewellynMath;
using LlewellynMath.NoiseGenerators;
using LlewellynMedia;

namespace Fountain.Forms
{
	public partial class MainWindow : Form
	{
		private bool undoing;
		private ActionQueue undoQueue = new ActionQueue(2048);
		private bool redoing;
		private ActionQueue redoQueue = new ActionQueue(2048);
		private Vector2 lastPointOnRenderArea;
		private Vector2 lastPointOnRender;

		private EffectDialog effectDialog;
		private BrushDialog brushDialog;
		private GeneratorDialog generatorDialog;

		[STAThread]
		private static void Main(params string[] args)
		{
			MainWindow mw;
			if (args.Length > 0) mw = new MainWindow(args[0]);
			else mw = new MainWindow();
			Application.EnableVisualStyles();
			Application.Run(mw);
		}
		public MainWindow(string filePath = null)
		{
			CenterToScreen();
			InitializeComponent();

			effectDialog = new EffectDialog(this);
			brushDialog = new BrushDialog(this);
			generatorDialog = new GeneratorDialog(this);

			renderArea.MouseWheel += renderArea_MouseWheel;
			Application.Idle += Application_Idle;

			Document.RenderSet += Document_RenderSet;
			Document.RenderRemoved += Document_RenderRemoved;
			Document.GradientSet += Document_GradientSet;
			Document.GradientRemoved += Document_GradientRemoved;
			Document.BrushSet += Document_BrushSet;
			Document.BrushRemoved += Document_BrushRemoved;
			Document.SelectedRenderChanged += Document_SelectedRenderChanged;
			Document.EffectSelected += Document_EffectSelected;
			Document.EffectDeselected += Document_EffectDeselected;
			Document.SelectedGradientChanged += Document_SelectedGradientChanged;
			Document.GeneratorSet += Document_GeneratorSet;
			Document.GeneratorRemoved += Document_GeneratorRemoved;

			if (filePath != null) HandleFileLoad(filePath);
		}

		private void Document_GeneratorRemoved(string name, HeightRender.Generator generator)
		{
			generatorListBox.Items.Clear();
			foreach (string s in Document.GeneratorNames)
				generatorListBox.Items.Add(s);
		}
		private void Document_GeneratorSet(string name, HeightRender.Generator generator)
		{
			generatorListBox.Items.Clear();
			foreach (string s in Document.GeneratorNames)
				generatorListBox.Items.Add(s);
		}
		private void Document_EffectDeselected(string name)
		{
			selectedEffectList.Items.Clear();
			foreach (string s in Document.SelectedEffectNames)
				selectedEffectList.Items.Add(s);
			UpdateRender();
		}
		private void Document_EffectSelected(string name)
		{
			selectedEffectList.Items.Clear();
			foreach (string s in Document.SelectedEffectNames)
				selectedEffectList.Items.Add(s);
			UpdateRender();
		}
		private void Document_SelectedRenderChanged(string name)
		{
			if (Document.SelectedRender != null)
			{
				renderArea.Image = Document.SelectedRender.Bitmap;
				undoQueue.Clear();
				redoQueue.Clear();
				selectedRenderBox.Text = name;
			}
			else
			{
				renderArea.Image = null;
				selectedRenderBox.Text = "";
			}
			UpdateRender();
		}
		private void Document_BrushRemoved(string name, HeightBrush brush)
		{
			leftBrushNameBox.Items.Clear();
			rightBrushNameBox.Items.Clear();
			foreach (string s in Document.BrushNames)
			{
				leftBrushNameBox.Items.Add(s);
				rightBrushNameBox.Items.Add(s);
			}
			leftBrushNameBox.SelectedItem = Document.LeftBrushName;
			rightBrushNameBox.SelectedItem = Document.RightBrushName;
		}
		private void Document_BrushSet(string name, HeightBrush brush)
		{
			leftBrushNameBox.Items.Clear();
			rightBrushNameBox.Items.Clear();
			foreach (string s in Document.BrushNames)
			{
				leftBrushNameBox.Items.Add(s);
				rightBrushNameBox.Items.Add(s);
			}
			leftBrushNameBox.SelectedItem = Document.LeftBrushName;
			rightBrushNameBox.SelectedItem = Document.RightBrushName;
		}
		private void Document_GradientRemoved(string name, PhotonGradient gradient)
		{
			selectedGradientBox.Items.Clear();
			foreach (string s in Document.GradientNames)
			{
				selectedGradientBox.Items.Add(s);
			}
			selectedGradientBox.SelectedItem = Document.SelectedGradient;
		}
		private void Document_GradientSet(string name, PhotonGradient gradient)
		{
			selectedGradientBox.Items.Clear();
			foreach (string s in Document.GradientNames)
			{
				selectedGradientBox.Items.Add(s);
			}
			selectedGradientBox.Text = Document.SelectedGradientName;
		}
		private void Document_RenderRemoved(string name, HeightRender render)
		{
			selectedRenderBox.Items.Clear();
			foreach (string s in Document.RenderNames)
			{
				selectedRenderBox.Items.Add(s);
			}
			selectedRenderBox.Text = Document.SelectedRenderName;
		}
		private void Document_RenderSet(string name, HeightRender render)
		{
			selectedRenderBox.Items.Clear();
			foreach (string s in Document.RenderNames)
			{
				selectedRenderBox.Items.Add(s);
			}
			selectedRenderBox.SelectedItem = Document.SelectedRender;
		}
		private void Document_SelectedGradientChanged(string name)
		{
			UpdateRender();
		}

		//Render Area
		private void renderArea_KeyDown(object sender, KeyEventArgs e)
		{
			#region Reset Zoom and Pan
			if (e.KeyCode == Keys.Space)
			{
				renderArea.ImageScale = Vector2.One;
				renderArea.ImageOffset = Vector2.Zero;
			}
			#endregion
			#region Undo
			if (e.Control && e.KeyCode == Keys.Z)
			{
				undoing = true;
			}
			#endregion
			#region Redo
			if (e.Control && e.KeyCode == Keys.Y)
			{
				redoing = true;
			}
			#endregion
		}
		private void renderArea_KeyUp(object sender, KeyEventArgs e)
		{
			//Undo
			if (undoing && (e.Control || e.KeyCode == Keys.Z))
			{
				undoing = false;
			}
			//Redo
			if (redoing && (e.Control || e.KeyCode == Keys.Y))
			{
				redoing = false;
			}
		}
		private void renderArea_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta != 0)
			{
				renderArea.ImageScale *= Numerics.Pow(1.1f, Numerics.Clamp(e.Delta, -2, 2));
			}
		}
		private void Application_Idle(object sender, EventArgs e)
		{
			Vector2 pointOnRenderArea = Numerics.ToVector(renderArea.PointToClient(Control.MousePosition));
			Vector2 pointOnRender = renderArea.ClientToImage(pointOnRenderArea);

			if (renderArea.Focused && Document.SelectedRender != null)
			{
				#region Undo
				if (undoing) Undo();
				#endregion
				#region Redo
				if (redoing) Redo();
				#endregion
				#region Panning
				if (MouseButtons == MouseButtons.Middle)
				{
					renderArea.ImageOffset += (pointOnRenderArea - lastPointOnRenderArea) / renderArea.ImageScale;
				}
				#endregion
				#region Brush
				//Select the appropriate brush based on the active mouse buttons.
				HeightBrush activeBrush = null;
				if (MouseButtons == MouseButtons.Left) activeBrush = Document.LeftBrush;
				else if (MouseButtons == MouseButtons.Right) activeBrush = Document.RightBrush;
				//If a mouse button was in use; paint.
				if (activeBrush != null)
				{
					HeightRender render = Document.SelectedRender;
					PhotonGradient gradient = Document.SelectedGradient;
					IEnumerable<HeightRender.Effect> effects;
					if (paintEffectsBox.Checked) effects = Document.SelectedEffects;
					else effects = null;//If we aren't painting with effects then we don't care about supplying them to the update.
					#region Process Steps
					//The brush stroke is broken up into steps that are each a given length (in pixels). The length is denoted by the brush precision.
					Vector2 brushDelta = lastPointOnRender - pointOnRender;
					float strokeLength = (float)brushDelta.Length;
					float steps = strokeLength / activeBrush.Precision + 1;
					Vector2 brushStep = brushDelta * (1.0f / steps);
					for (int i = (int)steps - 1; i >= 0; i--)//Work backwards so the undo and redo functionality makes sense.
					{
						//Calculate the current brush position, based on the starting point, the step vector and the current step index.
						Vector2 brushPosition = pointOnRender + brushStep * i;
						#region Process Brush Paint
						try
						{
							//Perform the actual brush operation and capture the selected area it affects.
							FieldSelection brushArea;
							float[] previousData;
							activeBrush.Paint(render.HeightField, (int)brushPosition.X, (int)brushPosition.Y, strokeLength, out brushArea, out previousData);
							//Add this paint event to the undo queue.
							undoQueue.Enqueue(new BrushAction(brushArea, previousData));
							redoQueue.Clear();
							//Break up the brush area into multiple parts if they intersect the edges of the image.
							foreach (FieldSelection fs in brushArea.SubSelectionsOf(render.HeightField))
							{
								//Omit any parts that have a width or height of zero.
								if (!fs.IsEmpty)
								{
									//Update the corresponding part of the render.
									render.UpdateArea(fs.Left, fs.Top, fs.Width, fs.Height, gradient, effects);
									//Invalidate the corresponding part of the image panel so that it redraws itself in realtime.
									Point start = Numerics.ToPoint(renderArea.ImageToClient(new Vector2(fs.Left, fs.Top)));
									Point end = Numerics.ToPoint(renderArea.ImageToClient(new Vector2(fs.Right, fs.Bottom)));
									renderArea.Invalidate(new Rectangle(Numerics.Max(start.X, 0), Numerics.Max(start.Y, 0), Numerics.Max(end.X, 0), Numerics.Max(end.Y, 0)));
								}
							}
						}
						catch (Exception ex)
						{
							//This usually happens when activeBrush.Paint is called, because it executes the user's brush script. Shows the script error message as a message box.
							MessageBox.Show(ex.Message, "There was a runtime error with your brush script.");
						}
						#endregion
					}
					#endregion
				}
				#endregion
			}
			else undoing = redoing = false;

			lastPointOnRenderArea = pointOnRenderArea;
			lastPointOnRender = pointOnRender;
		}
		//Document
		private void newDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.IsEmpty || MessageBox.Show("Are you sure you want to create a new document?\nAll progress will be lost...", "New Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
				Document.Clear();
		}
		private void openDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.IsEmpty || MessageBox.Show("Are you sure you want to open another document?\nAll progress will be lost...", "Open Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				openFileDialog.Filter = "Fountain Document (*.fdf)|*.fdf";
				openFileDialog.FileName = Document.AssociatedPath;
				if (openFileDialog.ShowDialog() == DialogResult.OK) HandleFileLoad(openFileDialog.FileName);
			}
		}
		private void saveDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.AssociatedPath != null)
			{
				HandleFileSave(Document.AssociatedPath);
			}
			else saveDocumentAsToolStripMenuItem_Click(sender, e);
		}
		private void saveDocumentAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveFileDialog.Filter = "Fountain Document (*.fdf)|*.fdf";
			if (saveFileDialog.ShowDialog() == DialogResult.OK) HandleFileSave(saveFileDialog.FileName);
		}
		//Renders
		private void newRenderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextEntryDialog ted = new TextEntryDialog("Render Name");
			if (ted.ShowDialog() == DialogResult.OK)
			{
				if (!Document.ContainsRender(ted.Entry))
				{
					if (Document.SetRender(ted.Entry, null))
					{
						RenderDialog rd = new RenderDialog(ted.Entry, this);
						if (rd.ShowDialog() == DialogResult.OK)
						{
							Document.SetRender(ted.Entry, new HeightRender(rd.RenderWidth, rd.RenderHeight, rd.RenderClamp, rd.RenderClampMin, rd.RenderClampMax, rd.RenderWrapX, rd.RenderWrapY));
							Document.SelectedRenderName = ted.Entry;
						}
						else Document.RemoveRender(ted.Entry);
					}
					else MessageBox.Show("Please enter a valid name for your render in the box above.", "Naming Error");
				}
				else MessageBox.Show(string.Format("A render named {0} already exists. Type a new name in the box above, or remove the existing render.", ted.Entry), "Naming Conflict");
			}
		}
		private void editRenderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.SelectedRender != null)
			{
				RenderDialog rd = new RenderDialog(Document.SelectedRenderName, this);
				rd.ShowDialog();
			}
		}
		private void clearRenderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.SelectedRender != null && MessageBox.Show(string.Format("Are you sure you want to clear {0}? You cannot undo this action.", Document.SelectedRenderName), "Clear Render", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Document.SelectedRender.Clear();
				Document.SelectedRender.UpdateAll(Document.SelectedGradient, Document.SelectedEffects);
				renderArea.Invalidate();
				undoQueue.Clear();
				redoQueue.Clear();
			}
		}
		private void selectedRenderBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Document.SelectedRenderName = selectedRenderBox.Text;
			Document.SelectedRender.UpdateAll(Document.SelectedGradient, Document.SelectedEffects);
			renderArea.Invalidate();
		}
		//Gradients
		private void newGradientToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextEntryDialog ted = new TextEntryDialog("Gradient Name");
			if (ted.ShowDialog() == DialogResult.OK)
			{
				if (!Document.ContainsGradient(ted.Entry))
				{
					if (ted.Entry != null && ted.Entry.Length > 0)
					{
						GradientDialog gd = new GradientDialog(ted.Entry, this);
						if (gd.ShowDialog() != DialogResult.OK) Document.RemoveGradient(ted.Entry);
					}
					else MessageBox.Show("Please enter a valid name for your gradient in the box above.", "Naming Error");
				}
				else MessageBox.Show(string.Format("A gradient named {0} already exists. Type a new name in the box above, or remove the existing gradient.", ted.Entry), "Naming Conflict");
			}
		}
		private void editGradientToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.SelectedGradient != null)
			{
				PhotonGradient fallback = Document.SelectedGradient.MakeCopy();
				GradientDialog gd = new GradientDialog(Document.SelectedGradientName, this);
				if (gd.ShowDialog() == DialogResult.OK && Document.SelectedRender != null)
				{
					Document.SelectedRender.UpdateAll(Document.SelectedGradient, Document.SelectedEffects);
					renderArea.Invalidate();
				}
				else Document.SetGradient(Document.SelectedGradientName, fallback);
			}
		}
		private void selectedGradientBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Document.SelectedGradientName = selectedGradientBox.Text;
		}
		//Effects
		private void newEffectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextEntryDialog ted = new TextEntryDialog("Effect Name");
			if (ted.ShowDialog() == DialogResult.OK)
			{
				if (!Document.ContainsEffect(ted.Entry))
				{
					if (Document.SetEffect(ted.Entry, null))
					{
						effectDialog.EffectName = ted.Entry;
						effectDialog.Show();
					}
					else MessageBox.Show("Please enter a valid name for your effect in the box above.", "Naming Error");
				}
				else MessageBox.Show(string.Format("An effect named {0} already exists. Type a new name in the box above, or remove the existing effect.", ted.Entry), "Naming Conflict");
			}
		}
		private void editEffectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			effectDialog.Show();
		}
		private void selectedEffectList_DoubleClick(object sender, EventArgs e)
		{
			Document.DeselectEffect((string)selectedEffectList.SelectedItem);
		}
		//Brushes
		private void newBrushToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextEntryDialog ted = new TextEntryDialog("Brush Name");
			if (ted.ShowDialog() == DialogResult.OK)
			{
				if (!Document.ContainsBrush(ted.Entry))
				{
					if (Document.SetBrush(ted.Entry, new HeightBrush(64, 64, 1, 8)))
					{
						brushDialog.BrushName = ted.Entry;
						brushDialog.Show();
					}
					else MessageBox.Show("A brush cannot have an empty name.", "Naming Error");
				}
				else MessageBox.Show(string.Format("A brush named {0} already exists. Type a new name in the box above, or remove the existing brush.", ted.Entry), "Naming Conflict");
			}
		}
		private void editBrushToolStripMenuItem_Click(object sender, EventArgs e)
		{
			brushDialog.Show();
		}
		private void leftBrushNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Document.LeftBrushName = leftBrushNameBox.Text;
		}
		private void rightBrushNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Document.RightBrushName = rightBrushNameBox.Text;
		}
		//Generators
		private void newGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextEntryDialog ted = new TextEntryDialog("Dialog Name");
			if (ted.ShowDialog() == DialogResult.OK)
			{
				if (!Document.ContainsGenerator(ted.Entry))
				{
					if (Document.SetGenerator(ted.Entry, null))
					{
						generatorDialog.GeneratorName = ted.Entry;
						generatorDialog.Show();
					}
					else MessageBox.Show("Please enter a valid name for your generator in the box above.", "Naming Error");
				}
				else MessageBox.Show(string.Format("A generator named {0} already exists. Type a new name in the box above, or remove the existing generator.", ted.Entry), "Naming Conflict");
			}
		}
		private void editGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			generatorDialog.Show();
		}
		private void executeGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Document.SelectedRender != null && Document.ContainsGenerator((string)generatorListBox.SelectedItem))
			{
				HeightRender.Generator generator = Document.GetGenerator((string)generatorListBox.SelectedItem);
				if (generator != null && MessageBox.Show("Applying a generator will modify or replace the current height map. This action cannot be undone.\n\nWould you like to continue?", "Apply Generator", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					lock (undoQueue) undoQueue.Clear();
					lock (redoQueue) redoQueue.Clear();
					HeightField field = Document.SelectedRender.HeightField;
					lock (field)
					{
						try
						{
							for (int u = 0; u < field.Width; u++)
							{
								for (int v = 0; v < field.Height; v++)
								{
									field[u, v] = (float)generator(u, v, field);
								}
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "There was a runtime error with your generator script.");
						}
						finally
						{
							UpdateRender();
						}
					}
				}
			}
		}

		private void Undo()
		{
			if (undoQueue.Count > 0)
			{
				BrushAction undoAction = undoQueue.Dequeue();
				//Set all of the values in the affected area to what they were before the brush action took place.
				HeightRender render = Document.SelectedRender;
				BrushAction opposite = new BrushAction(undoAction.Selection, undoAction.Data);
				for (int x = undoAction.Selection.Left; x < undoAction.Selection.Right; x++)
				{
					for (int y = undoAction.Selection.Top; y < undoAction.Selection.Bottom; y++)
					{
						float oldSample;
						if (render.HeightField.TryGetHeight(x, y, out oldSample))
						{
							int i = (y - undoAction.Selection.Top) * undoAction.Selection.Width + (x - undoAction.Selection.Left);
							render.HeightField[x, y] = undoAction.Data[i];
							opposite.Data[i] = oldSample;
						}
					}
				}
				PhotonGradient gradient = Document.SelectedGradient;
				IEnumerable<HeightRender.Effect> effects;
				if (paintEffectsBox.Checked) effects = Document.SelectedEffects;
				else effects = null;//Don't worry about effects if we're not painting with them.
				//Update each section of the selection when wrapping has been accounted for.
				foreach (FieldSelection fs in undoAction.Selection.SubSelectionsOf(render.HeightField))
				{
					//Omit any parts that have a width or height of zero.
					if (!fs.IsEmpty)
					{
						//Update the corresponding part of the render.
						render.UpdateArea(fs.Left, fs.Top, fs.Width, fs.Height, gradient, effects);
						//Invalidate the corresponding part of the image panel so that it redraws itself in realtime.
						Point start = Numerics.ToPoint(renderArea.ImageToClient(new Vector2(fs.Left, fs.Top)));
						Point end = Numerics.ToPoint(renderArea.ImageToClient(new Vector2(fs.Right, fs.Bottom)));
						renderArea.Invalidate(new Rectangle(Numerics.Max(start.X, 0), Numerics.Max(start.Y, 0), Numerics.Max(end.X, 0), Numerics.Max(end.Y, 0)));
					}
				}
				redoQueue.Enqueue(opposite);
			}
		}
		private void Redo()
		{
			if (redoQueue.Count > 0)
			{
				BrushAction redoAction = redoQueue.Dequeue();
				//Set all of the values in the affected area to what they were before the undo action took place.
				HeightRender render = Document.SelectedRender;
				BrushAction opposite = new BrushAction(redoAction.Selection, redoAction.Data);
				for (int x = redoAction.Selection.Left; x < redoAction.Selection.Right; x++)
				{
					for (int y = redoAction.Selection.Top; y < redoAction.Selection.Bottom; y++)
					{
						float oldSample;
						if (render.HeightField.TryGetHeight(x, y, out oldSample))
						{
							int i = (y - redoAction.Selection.Top) * redoAction.Selection.Width + (x - redoAction.Selection.Left);
							render.HeightField[x, y] = redoAction.Data[i];
							opposite.Data[i] = oldSample;
						}
					}
				}
				PhotonGradient gradient = Document.SelectedGradient;
				IEnumerable<HeightRender.Effect> effects;
				if (paintEffectsBox.Checked) effects = Document.SelectedEffects;
				else effects = null;//Don't worry about effects if we're not painting with them.
				//Update each section of the selection when wrapping has been accounted for.
				foreach (FieldSelection fs in redoAction.Selection.SubSelectionsOf(render.HeightField))
				{
					//Omit any parts that have a width or height of zero.
					if (!fs.IsEmpty)
					{
						//Update the corresponding part of the render.
						render.UpdateArea(fs.Left, fs.Top, fs.Width, fs.Height, gradient, effects);
						//Invalidate the corresponding part of the image panel so that it redraws itself in realtime.
						Point start = Numerics.ToPoint(renderArea.ImageToClient(new Vector2(fs.Left, fs.Top)));
						Point end = Numerics.ToPoint(renderArea.ImageToClient(new Vector2(fs.Right, fs.Bottom)));
						renderArea.Invalidate(new Rectangle(Numerics.Max(start.X, 0), Numerics.Max(start.Y, 0), Numerics.Max(end.X, 0), Numerics.Max(end.Y, 0)));
					}
				}
				undoQueue.Enqueue(opposite);
			}
		}
		private void UpdateRender()
		{
			if (Document.SelectedRender != null)
			{
				try
				{
					Document.SelectedRender.UpdateAll(Document.SelectedGradient, Document.SelectedEffects);
				}
				catch (Exception e)
				{
					//This usually happens when Apply is called, because it executes the user's effect script. Shows the script error message as a message box.
					MessageBox.Show(e.Message, "There was a runtime error with one of your effect scripts.");
				}
				finally
				{
					renderArea.Invalidate();
				}
			}
		}

		private static void HandleFileLoad(string path)
		{
			if (path != null)
			{
				switch (Document.Load(path))
				{
					case Document.IOEvaluation.CannotOpenStream:
						MessageBox.Show(string.Format("The file at {0} could not be loaded. This was likely caused by a permissions issue - try running Fountain as Administrator.", path), "Cannot Open Stream");
						break;
					case Document.IOEvaluation.ConversionError:
						MessageBox.Show(string.Format("The file at {0} could not be loaded. The file may be corrupt, or the file standard may have changed.", path), "File Parsing Error");
						break;
					case Document.IOEvaluation.FileDoesNotExist:
						MessageBox.Show(string.Format("There was no file located at {0}.", path), "File Does Not Exist");
						break;
				}
			}
			else MessageBox.Show("The supplied path was null.", "File Path Null");
		}
		private static void HandleFileSave(string path)
		{
			if (path != null)
			{
				switch (Document.Save(path))
				{
					case Document.IOEvaluation.CannotOpenStream:
						MessageBox.Show(string.Format("The file could not be saved to {0}. This was likely caused by a permissions issue - try running Fountain as Administrator.", path), "Cannot Open Stream");
						break;
					case Document.IOEvaluation.ConversionError:
						MessageBox.Show(string.Format("The file could not be saved to {0}. The document may be corrupt.", path), "Document Conversion Error");
						break;
				}
			}
			else MessageBox.Show("The supplied path was null.", "File Path Null");
		}

		private class BrushAction
		{
			private FieldSelection selection;
			public FieldSelection Selection
			{
				get
				{
					return selection;
				}
			}
			private float[] data;
			public float[] Data
			{
				get
				{
					return data;
				}
			}

			public BrushAction(FieldSelection selection, float[] data)
			{
				this.selection = selection;
				this.data = data;
			}
		}
		private class ActionQueue
		{
			private uint capacity;
			public uint Capacity
			{
				get
				{
					return capacity;
				}
				set
				{
					capacity = value;
					Trim();
				}
			}
			private LinkedList<BrushAction> list = new LinkedList<BrushAction>();
			public int Count
			{
				get
				{
					return list.Count;
				}
			}

			public ActionQueue(uint capacity)
			{
				this.capacity = capacity;
			}

			private void Trim()
			{
				if (Count > capacity)
					for (uint i = capacity; i < Count; i++)
						list.RemoveFirst();
			}

			public void Enqueue(BrushAction action)
			{
				list.AddLast(action);
				Trim();
			}
			public BrushAction Dequeue()
			{
				BrushAction ba = list.Last.Value;
				list.RemoveLast();
				return ba;
			}
			public void Clear()
			{
				list.Clear();
			}
		}
	}
}