using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrazimMestra.Dtos
{
    public class BasicUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
