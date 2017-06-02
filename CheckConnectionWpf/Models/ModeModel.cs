using System;

namespace CheckConnectionWpf.Models
{
    public enum Mode{ Simple = 0 , Advanced  }

    public class ModeModelEventArgs : EventArgs
    {
        public ModeModel ModeModel { get; set; }
        public ModeModelEventArgs(ModeModel modeModel)
        {
            ModeModel = modeModel;
        }
    }
    public class ModeModel
    {
        public event EventHandler<ModeModelEventArgs> ModeChanged;
        public Mode Id { get; set; }
        public ModeModel(Mode id)
        {
            Id = id;            
        }
        public void ModeChange(Mode id)
        {
            Id = id;
            ModeChanged(this, new ModeModelEventArgs(this));
        }
    }
}
