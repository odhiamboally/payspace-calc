using PS.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Tests.RepositoryTests.MockRepositories;

internal sealed class MockCountryRepository
{
    public static List<Country> GetMockCountries()
    {
        return new List<Country>
        {
            new Country
            {
                Id = 1,
                Description = "South Africa",
                Code = "RSA",
                TaxRegime = "PROG",
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket
                    {
                        TaxBracketLines = new List<TaxBracketLine>
                        {
                            new TaxBracketLine
                            {
                                Id = 1,
                                TaxBracketId = 1,
                                OrderNumber = 1,
                                LowerLimit = 0,
                                UpperLimit = 237100,
                                Rate = 18
                            },
                            new TaxBracketLine
                            {
                                Id = 2,
                                TaxBracketId = 1,
                                OrderNumber = 2,
                                LowerLimit = 237100,
                                UpperLimit = 370500,
                                Rate = 26
                            }
                        }
                    }
                }
            },
            new Country
            {
                Id = 2,
                Description = "United States of America",
                Code = "USA",
                TaxRegime = "PROG",
                TaxBrackets = new List<TaxBracket>
                {
                    new TaxBracket
                    {
                        TaxBracketLines = new List<TaxBracketLine>
                        {
                            new TaxBracketLine
                            {
                                Id = 8,
                                TaxBracketId = 2,
                                OrderNumber = 1,
                                LowerLimit = 0,
                                UpperLimit = 11600,
                                Rate = 10
                            },
                            new TaxBracketLine
                            {
                                Id = 9,
                                TaxBracketId = 2,
                                OrderNumber = 2,
                                LowerLimit = 11600,
                                UpperLimit = 47150,
                                Rate = 12
                            }
                        }
                    }
                }
            },
            new Country
            {
                Id = 3,
                TaxRegime = "FLAT",
                TaxRates = new List<TaxRate>
                {
                    new TaxRate
                    {
                        Id = 1,
                        CountryId = 3,
                        RateCode = "FLATRATE",
                        Rate = 20000
                    },
                    new TaxRate
                    {
                        Id = 2,
                        CountryId = 3,
                        RateCode = "THRES",
                        Rate = 150000
                    }
                }
            },
            new Country
            {
                Id = 4,
                TaxRegime = "PERC",
                TaxRates = new List<TaxRate>
                {
                    new TaxRate
                    {
                        Id = 3,
                        CountryId = 4,
                        RateCode = "TAXPERC",
                        Rate = 30
                    }
                }
            },
            new Country
            {
                Id = 5,
                TaxRegime = "PERC",
                TaxRates = new List<TaxRate>
                {
                    new TaxRate
                    {
                        Id = 3,
                        CountryId = 4,
                        RateCode = "TAXPERC",
                        Rate = 30
                    }
                }
            },
            new Country
            {
                Id = 6,
                TaxRegime = "UNKNOWN",
                TaxRates = new List<TaxRate>
                {
                    new TaxRate
                    {
                        Id = 3,
                        CountryId = 4,
                        RateCode = "TAXPERC",
                        Rate = 30
                    }
                }
            }
        };
    }
}
