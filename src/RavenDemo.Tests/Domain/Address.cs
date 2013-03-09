namespace RavenDemo.Tests.Domain
{
    public abstract class Address
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}