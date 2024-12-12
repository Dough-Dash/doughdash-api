using doughdash_api.Data;

namespace doughdash_api.Utility;

public class Authorization
{
    private readonly DoughDashContext _context;

    public Authorization(DoughDashContext context)
    {
        _context = context;
    }

    public bool CheckAccessCode(string code)
    {
        var accessCode = _context.AccessCodes.FirstOrDefault(e => e.Code == code);
        return accessCode != null;
    }
}