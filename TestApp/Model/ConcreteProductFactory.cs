namespace TestApp.Model
{
    public class ConcreteProductFactory : ProductFactory
    {
        public override Product Create()
        {
            return new Product();
        }
    }
}
