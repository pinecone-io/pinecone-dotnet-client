using OneOf;

namespace Pinecone.Client;

public class MetadataValue(
    OneOf<double, string, bool, IEnumerable<double>, IEnumerable<string>, IEnumerable<bool>> input)
    : OneOfBase<double, string, bool, IEnumerable<double>, IEnumerable<string>, IEnumerable<bool>>(input);