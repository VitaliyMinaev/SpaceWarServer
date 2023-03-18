namespace SpaceWar.Api.Domain.Abstract
{
    public abstract class BaseDomain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}