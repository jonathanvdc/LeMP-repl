# LeMP-repl

`LeMP-repl` is a super simple interactive program that that reads unprocessed EC#, LES v2 or LES v3 code as input and produces processed or unprocessed EC#, LES v2 or LES v3 code as output.

The main motivation for building this tool is that it makes it much easier to look at and reason about EC# syntax trees.

## Build instructions

`LeMP-repl` is fairly straightforward to build. Just do this following:

```
$ nuget restore
$ msbuild /p:Configuration=Release
```

## Options

`LeMP-repl` tries to fulfill a simple use case, so there aren't a lot of options. Here's a full list, adapted from `--help`:

### Input and output

  * `--input-language=⟨language⟩, -i⟨language⟩`

    Selects an input language, which can be either `ecs`, `les`, `les2` or `les3`. By default, the input language is EC#.

  * `--no-process-macros, -p`

    Turns off macro processing by LeMP.

  * `--output-language=⟨language⟩, -o⟨language⟩`

    Selects an output language, which can be either `cs`, `ecs`, `les`, `les2` or `les3`. By default, the output language is LES v3.

### Overall options

  * `--help, -h, -?`

    Display a help message and exit.
