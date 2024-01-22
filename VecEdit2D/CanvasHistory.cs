using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace VecEdit2D
{
    internal class CanvasHistory
    {
        public class CanvasSnapshot
        {
            public Image canvas { get; private set; }

            public CanvasSnapshot(Image _canvas)
            {
                canvas = Image.DeepCopy(_canvas);
            }
        }
        public class CanvasCaretaker
        {
            private Stack<CanvasSnapshot> undoStack = new Stack<CanvasSnapshot>();
            private Stack<CanvasSnapshot> redoStack = new Stack<CanvasSnapshot>();

            public void SaveState(Image _canvas)
            {
                undoStack.Push(new CanvasSnapshot(_canvas));
                redoStack.Clear();

                if (undoStack.Count > 20)
                {
                    undoStack.Pop();
                }
            }

            public Image Undo()
            {
                if (undoStack.Any())
                {
                    redoStack.Push(undoStack.Pop());
                    if (undoStack.Any())
                    {
                        return undoStack.Peek().canvas;
                    }
                }

                return null;
            }

            public Image Redo()
            {
                if (redoStack.Any())
                {
                    var snapshot = redoStack.Pop();
                    undoStack.Push(snapshot);
                    return snapshot.canvas;
                }

                return null;
            }
        }
    }
}
