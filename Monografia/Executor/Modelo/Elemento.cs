//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Executor.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Elemento
    {
        public Elemento()
        {
            this.DadosTreinamento = new HashSet<DadosTreinamento>();
            this.DadosValidacao = new HashSet<DadosValidacao>();
        }
    
        public int Codigo { get; set; }
        public double ValorMinimo { get; set; }
        public double ValorIdeal { get; set; }
        public double ValorMaximo { get; set; }
        public string Descricao { get; set; }
    
        public virtual ICollection<DadosTreinamento> DadosTreinamento { get; set; }
        public virtual ICollection<DadosValidacao> DadosValidacao { get; set; }
    }
}
