﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Application.Dtos.TaxBracketLine;


public record UpdateTaxBracketLineRequest
{
    public int Id { get; init; }
    public int OrderNumber { get; init; }
    public decimal LowerLimit { get; init; }
    public decimal UpperLimit { get; init; }
    public decimal Rate { get; init; }
}
