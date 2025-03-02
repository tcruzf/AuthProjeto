using System.ComponentModel;

namespace ControllRR.Domain.Enums
{
    public enum NFeSource
    {
        [Description("Nacional")]
        Nacional = 0,

        [Description("Estrangeira compra direta")]
        Estrangeira_Compra_Direta = 1,

        [Description("Estrangeira compra mercado interno")]
        Estrangeira_Compra_Mercado_Interno = 2
    }
}