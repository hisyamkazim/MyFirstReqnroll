using Bogus;
using Reqnroll;
using System.Collections.Generic;

namespace MyFirstReqnroll.Helpers;


interface IFakeField
{
    public string Generate();
}

class FakeFullName : IFakeField
{
    private readonly Faker faker;
    public FakeFullName(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Name.FullName();
    }
}

class FakeFirstName : IFakeField
{
    private readonly Faker faker;
    public FakeFirstName(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Name.FirstName();
    }
}

class FakeLastName : IFakeField
{
    private readonly Faker faker;
    public FakeLastName(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Name.LastName();
    }
}

class FakeUsername : IFakeField
{
    private readonly Faker faker;
    public FakeUsername(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Internet.UserName();
    }
}

class FakePassword : IFakeField
{
    private readonly Faker faker;
    public FakePassword(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Internet.Password();
    }
}

class FakeEmail : IFakeField
{
    private readonly Faker faker;
    public FakeEmail(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Internet.Email();
    }
}
class FakeRandom : IFakeField
{
    private readonly Faker faker;
    public FakeRandom(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.AlphaNumeric(5);
    }
}

class FakeData : IFakeField
{
    private readonly Faker faker;
    public FakeData(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.Replace("Qualysoft-??####??");
    }
}

class FakeAmount : IFakeField
{
    private readonly Faker faker;
    public FakeAmount(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.Replace("5#######");
    }
}

class FakeNumber : IFakeField
{
    private readonly Faker faker;
    public FakeNumber(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.Number(1000, 9999).ToString();
    }
}

class FakeProduct : IFakeField
{
    private readonly Faker faker;
    public FakeProduct(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Commerce.Product();
    }
}

class FakeNikOrPassport : IFakeField
{
    private readonly Faker faker;
    public FakeNikOrPassport(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.Replace("################");
    }
}

class FakeSentence : IFakeField
{
    private readonly Faker faker;
    public FakeSentence(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Lorem.Sentence(4, 3);
    }
}

class FakeAddress : IFakeField
{
    private readonly Faker faker;
    public FakeAddress(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Address.FullAddress();
    }
}

class FakePhoneNumber : IFakeField
{
    private readonly Faker faker;
    public FakePhoneNumber(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Phone.PhoneNumber();
    }
}

class FakeRank : IFakeField
{
    private readonly Faker faker;
    public FakeRank(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.Number(1, 100).ToString();
    }
}

class FakeCompany : IFakeField
{
    private readonly Faker faker;
    public FakeCompany(Faker faker)
    {
        this.faker = faker;
    }

    public string Generate()
    {
        return faker.Random.Replace("PT. Automation Tester ") + faker.Random.Number(1, 1000);
    }
}

public class FakeFieldGenerator
{
    private readonly Dictionary<string, IFakeField> fields;
    private readonly Faker faker;
    private static FakeFieldGenerator _this;

    private FakeFieldGenerator()
    {
        faker = new Faker(locale: "id_ID");
        fields = new Dictionary<string, IFakeField>()
        {
            {"{{FakeFullName}}", new FakeFullName(faker)},
            {"{{FakeEmail}}", new FakeEmail(faker)},
            {"{{FakeRandom}}", new FakeRandom(faker)},
            {"{{FakeData}}", new FakeData(faker)},
            {"{{FakeAmount}}", new FakeAmount(faker)},
            {"{{FakeNumber}}", new FakeNumber(faker)},
            {"{{FakeProduct}}", new FakeProduct(faker)},
            {"{{FakeNikOrPassport}}", new FakeNikOrPassport(faker)},
            {"{{FakeSentence}}", new FakeSentence(faker)},
            {"{{FakeAddress}}", new FakeAddress(faker)},
            {"{{FakePhoneNumber}}", new FakePhoneNumber(faker)},
            {"{{FakeRank}}", new FakeRank(faker)},
            {"{{FakeCompany}}", new FakeCompany(faker)},
            {"{{FakeFirstName}}", new FakeFirstName(faker)},
            {"{{FakeLastName}}", new FakeLastName(faker)},
            {"{{FakeUsername}}", new FakeUsername(faker)},
            {"{{FakePassword}}", new FakePassword(faker)},
        };
    }

    public static FakeFieldGenerator CreateInstance()
    {
        if (_this == null)
        {
            _this = new FakeFieldGenerator();
        }

        return _this;
    }

    public Table FakeTable(Table table)
    {
        string[] headers = new string[table.Header.Count];
        var i = 0;
        foreach (var h in table.Header)
        {
            headers[i] = h;
            i++;
        }
        var result = new Table(headers);


        foreach (var row in table.Rows)
        {
            var newRows = new Dictionary<string, string>();

            foreach (var column in row)
            {
                newRows.Add(column.Key, Fake(column.Value));
            }

            result.AddRow(newRows);
        }

        return result;
    }

    public string Fake(string f)
    {
        try
        {
            var field = fields[f];
            return field.Generate();
        }
        catch (KeyNotFoundException)
        {
            return f;
        }
    }
}