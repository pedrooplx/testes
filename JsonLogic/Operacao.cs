namespace JsonLogic
{
    public class Operacao
    {
        public Header Header { get; set; }
        public object Payload { get; set; }

        public Operacao RetornaOperacao()
        {
            var produto = IteratorProduto.ObterProdutoAleatorio();
            return new Operacao
            {
                Header = new Header()
                {
                    Id = Guid.NewGuid().ToString(),
                    CorrelationId = Guid.NewGuid().ToString(),
                    Produto = produto,
                    Usuario = Faker.Name.First(),
                    Status = Faker.Currency.ThreeLetterCode(),
                    Versao = Faker.RandomNumber.Next().ToString()

                },
                Payload = RetornaBody(produto)
            };
        }

        public static object RetornaBody(string produto)
        {
            if (produto.Equals("FXSPOT"))
                return new { 
                    Name = Faker.Company.Name(), 
                    LoremParagraph = Faker.Lorem.GetFirstWord() ,
                    Bs = Faker.Company.BS() ,
                    Country = Faker.Country.Name() ,
                    Isin = Faker.Finance.Isin()
                };
            if (produto.Equals("NDFONSHORE"))
                return new { 
                    UserName = Faker.Internet.UserName(), 
                    Number = Faker.Phone.Number(),
                    Phone = Faker.Phone.Number(),
                    Mail = Faker.Internet.FreeEmail(),
                    Currency = Faker.Currency.Name()
                };
            
            return new { DateOfBirth = Faker.Identification.DateOfBirth(), Maturity = Faker.Finance.Maturity() };
        }
    }
    public class Header
    {
        public string Id { get; set; }
        public string CorrelationId { get; set; }
        public string Produto { get; set; }
        public string Usuario { get; set; }
        public string Status { get; set; }
        public string Versao { get; set; }
    }
}
