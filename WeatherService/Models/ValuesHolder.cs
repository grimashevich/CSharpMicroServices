namespace WeatherService.Models
{
    public class ValuesHolder
    {
        private List<string> _value;

        public ValuesHolder()
        {
            _value = new List<string>();
        }

        public void Add(string value)
        {
            _value.Add(value);
        }

        public string Get()
        {
            return string.Join(',', _value.ToArray());
        }

        public List<string> Values
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
