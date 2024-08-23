# Contributing

## Developing locally

You can set up your project and run tests with the following:

```sh
cd src
dotnet build
dotnet test
```

By default, the `dotnet test` command will attempt to read the `PINECONE_API_KEY`
environment varible to execute the suite of integration tests. If you want to
restrict the command to run a specific set of tests (e.g. the unit tests), you
can apply a filter like so:

```sh
dotnet test src --filter "FullyQualifiedName~Pinecone.Test.Unit"
```

## Generated SDK

While we value open-source contributions to this SDK, this library is generated programmatically.
Additions made directly to this library would have to be moved over to our generation code, otherwise
they would be overwritten upon the next generated release. Feel free to open a PR as a proof of concept,
but know that we will not be able to merge it as-is. We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README.md are always very welcome!

