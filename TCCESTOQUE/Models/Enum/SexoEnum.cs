using System.ComponentModel.DataAnnotations;

namespace TCCESTOQUE.Models.Enum
{
    public enum SexoEnum
    {
        Selecione = 0,
        Masculino,
        Feminino,
        [Display(Name = "Prefiro não informar")]
        Outros, //prefiro não informar
    }
}
