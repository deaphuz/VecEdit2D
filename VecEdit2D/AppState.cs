using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecEdit2D
{
    public class AppState
    {
        //Singleton
        private static AppState _instance;

        public int ShapeID;
        public ShapeGroup canvas { get; set; }
        private AppState()
        {
            ShapeID = 0;
        }

        public static AppState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppState();
                }
                return _instance;
            }
        }

        public void Reset()
        {
            _instance = new AppState() { ShapeID = 0 };
        }

    }
}
