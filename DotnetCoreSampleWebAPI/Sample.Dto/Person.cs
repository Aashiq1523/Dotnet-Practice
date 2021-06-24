namespace Sample.Dto
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isAdmin { get; set; } = false;
        public int addressid { get; set; }
        public Address address { get; set; }
    }
}
