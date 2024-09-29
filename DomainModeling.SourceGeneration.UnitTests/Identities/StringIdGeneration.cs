namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateIdentity<string>(nameof(StringIdGenerationId))]
public class StringIdGeneration(StringIdGenerationId id) : Entity<StringIdGenerationId>(id);
