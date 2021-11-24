namespace mh_backend
{
    public class WeatherForecast
    {
        public string door1 { get; set; }

        public string door2 { get; set; }

        public string door3 { get; set; }

        public int chosenDoor { get; set; }
        public int openDoor { get; set; }
        public int switchedDoor { get; set; }
        public bool success { get; set; }
    }
}