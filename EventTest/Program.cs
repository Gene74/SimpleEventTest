using System;

namespace EventTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Character c = new Character();
            c.Hit(3); //output -> 97
            c.Hit(100); // output -> 0
            c.Hit(10); // no output because we are already at 0
        }
    }


    public class Character
    {
        public Int32Property HP;
        
        public Character()
        {
            HP = new Int32Property();
            HP.OnChanged += OnHPChanged;
        }

        private void OnHPChanged(object sender, EventArgs e)
        {
            Console.WriteLine("HP changed to " + HP.Value.ToString());
        }

        public void Hit(int value)
        {
            HP.Value -= value;
        }

    }

    public class Int32Property
    {
        //EVENTS

        public event EventHandler OnChanged;
        protected virtual void changed(EventArgs e)
        {
            if (OnChanged != null)
                OnChanged(this, e);
        }

        //PROPERTIES

        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        private int _value;
        public int Value
        {
            get { return _value; }
            set {
                int normalizedValue = normalize(value);
                if (normalizedValue != _value)
                {
                    _value = normalizedValue;
                    changed(EventArgs.Empty);
                }
            }
        }

        //CONSTRUCTOR

        public Int32Property(int minValue = 0, int maxValue = 100, int initialValue = 100)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            //no event triggering at the point of creation
            _value = normalize(initialValue);
        }

        // PRIVATE METHODS

        /// <summary>
        /// Will keep the value within the limits of MinValue and MaxValue.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>normalized value</returns>
        protected virtual int normalize(int value)
        {
            if (value < MinValue)
                return MinValue;
            else if (value > MaxValue)
                return MaxValue;
            else
                return value;
        }
    }
}
