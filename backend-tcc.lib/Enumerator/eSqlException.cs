namespace backend_tcc.lib.Enumerator
{
    public enum eSqlException : int
    {
        [DescricaoEnumeradorAttribute("Registro duplicado")]
        Unique = 2627,
        [DescricaoEnumeradorAttribute("Chave duplicada")]
        PrimaryKey = 2601,
        [DescricaoEnumeradorAttribute("Relacionamento inválido")]
        ForeignKey = 547
    }
}
