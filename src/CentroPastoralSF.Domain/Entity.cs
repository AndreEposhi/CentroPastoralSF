namespace CentroPastoralSF.Domain
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
    }
}