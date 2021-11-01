using Microsoft.EntityFrameworkCore;

namespace crpt.Models
{
    [Keyless]
    public class Quote
    {
        public USD USD { get; set; }
    }
}