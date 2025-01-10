namespace DomainSpace.Model.Entity;

/// <summary>
/// Base entity
/// </summary>
public class BaseEntity : BaseEntity<Guid>
{ }

/// <summary>
/// Base entity
/// </summary>
/// <typeparam name="T">Primary key type</typeparam>
public class BaseEntity<T>
{
    /// <summary>
    /// Identifier
    /// </summary>
    public T Id { get; set; } = default!;

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Delete time
    /// </summary>
    public DateTime? DeleteTime { get; set; }
}
