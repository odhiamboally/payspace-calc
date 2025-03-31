using System.Data.SqlClient;
using System.Diagnostics;

string connectionString = "Data Source=ALLAN\\SQLSVR22EXPRESS;Initial Catalog=PaySpace;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";

Console.WriteLine("***********************************************");
Console.WriteLine("*                   Welcome                   *");
Console.WriteLine("***********************************************");
Console.WriteLine("Calculating....");

var calculationCount = 0;

using (SqlConnection conn = new SqlConnection(connectionString))
{
    string taxCalculationQueries = "select * from TaxCalculations";
    SqlCommand taxCalcCommand = new SqlCommand(taxCalculationQueries, conn);
    conn.Open();
    SqlDataReader taxCalcReader = taxCalcCommand.ExecuteReader();
    Stopwatch sw = Stopwatch.StartNew();
    try
    {
        while (taxCalcReader.Read())
        {
            calculationCount++;
            var countryId = int.Parse(taxCalcReader["CountryId"].ToString()!);
            var taxMethod = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "select * from Countries where Id = " + countryId;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                    taxMethod = reader["TaxRegime"].ToString();
                }
                finally
                {
                    reader.Close();
                }
            }

            var income = decimal.Parse(taxCalcReader["Income"].ToString()!);
            var tax = 0m;
            var netPay = 0m;
            switch (taxMethod)
            {
                case "PROG":
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "select * from TaxBracketLines l inner join TaxBrackets b on l.TaxBracketId = b.Id where B.CountryId = " + countryId + " order by OrderNumber";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                if (income > decimal.Parse(reader["LowerLimit"].ToString()!) && income < decimal.Parse(reader["UpperLimit"].ToString()!))
                                {
                                    var rate = decimal.Parse(reader["Rate"].ToString()!);
                                    tax = income * (rate / 100);
                                    netPay = income - tax;
                                }
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }

                    break;

                case "PERC":
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "select * from TaxRates where CountryId = " + countryId;
                        SqlCommand command = new SqlCommand(queryString, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        try
                        {
                            reader.Read();
                            var percentage = decimal.Parse(reader["Rate"].ToString()!);
                            tax = income * (percentage / 100);
                            netPay = income - tax;
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }

                    break;

                case "FLAT":
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "select * from TaxRates where CountryId = " + countryId;
                        SqlCommand command = new SqlCommand(queryString, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        try
                        {
                            var flatRate = 0m;
                            var minimumThreshold = 0m;
                            while (reader.Read())
                            {
                                if (reader["RateCode"].ToString() == "FLATRATE")
                                {
                                    flatRate = decimal.Parse(reader["Rate"].ToString()!);
                                }
                                else if (reader["RateCode"].ToString() == "THRES")
                                {
                                    minimumThreshold = decimal.Parse(reader["Rate"].ToString()!);
                                }
                            }

                            if (income > minimumThreshold)
                            {
                                tax = flatRate;
                                netPay = income - tax;
                            }
                            else
                            {
                                tax = 0m;
                                netPay = income;
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }

                    break;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "update TaxCalculations set CalculatedTax = " + tax + ", NetPay = " + netPay + " where Id = " + taxCalcReader["Id"];
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        sw.Stop();
        Console.WriteLine(calculationCount + " calculations completed in " + sw.ElapsedMilliseconds + "ms");
        Console.ReadKey();
    }
    finally
    {
        taxCalcReader.Close();
    }
}
