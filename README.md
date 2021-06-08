# Grunkle Norbert's Great Lost Fortune
This is a [Global Game Jam](https://globalgamejam.org/) 2021 entry for the theme
"Lost and Found" for the [University of Southampton
site](https://globalgamejam.org/2021/jam-sites/university-southampton).

Game engine: Unity \
Models: Blender

## Development Environment
### UnityYAMLMerge Setup (on Windows)
`UnityYAMLMerge.exe` can be found relative to your `Unity.exe` under
`Data/Tools`. Ensure it is in your `$PATH`, then add the following to your
`.git/config` file:
```
[merge]
    tool = unityyamlmerge
[mergetool "unityyamlmerge"]
    keepBackup = false
    cmd = unityyamlmerge merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```
Depending on what you have installed you may also have to [Add a custom merge
tool to
UnityYAMLMerge](https://gist.github.com/Ikalou/197c414d62f45a1193fd#custom-merge-tool-optional)
to fix unresolved conflicts.

Once set up, Unity YAML files can be merged in a semantically correct way by
running `git mergetool` after `git merge`.

[More info about Git and
Unity](https://gist.github.com/Ikalou/197c414d62f45a1193fd)
