using System;

namespace Identity.Domain.Aggregates.Company;

public class CompanySetting
{
    public string Key { get; set; }
    public string Value {
        get;
        set;
    }

    public string Remark { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CompanyId { get; set; }
}