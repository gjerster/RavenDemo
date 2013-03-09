namespace RavenDemo.Tests.Domain
{
    public class OrderLine
    {
        private readonly string _name;
        private readonly string _productNumber;
        private readonly int _quantity;

        public OrderLine(string productNumber, int quantity, string name)
        {
            _productNumber = productNumber;
            _quantity = quantity;
            _name = name;
        }


        public string ProductNumber
        {
            get { return _productNumber; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Quantity
        {
            get { return _quantity; }
        }
    }
}