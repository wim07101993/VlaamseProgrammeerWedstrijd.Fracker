namespace Fracking
{
    public class Ground
    {
        private bool _isHard;


        public bool OldValue { get; set; }

        public bool IsHard
        {
            get => _isHard;
            set
            {
                _isHard = value;
                HasChanged = true;
            }
        }

        public bool HasChanged { get; set; }

        public bool HasConnection { get; set; }
    }
}
