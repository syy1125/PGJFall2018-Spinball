# PGJFall2018-Spinball

*Sections:
[Project Information](#project-information),
[Public Feedback](#public-feedback),
[Contributing](#contributing)*

## Project Information

Spinball: Spiral Out Of Control (SpOOC) was originally developed for Proto Game Jam, hosted by Video Game Development Club (VGDC) of University of California, Irvine (UCI
Because of its popularity amongst the club at launch, we have decided to continue development.

The project's Trello board can be found at https://trello.com/b/EyfOZSGU.

## Public Feedback

Players are welcome to submit bug reports or ideas through GitHub issues.

If you wish to make a pull request, please read the [Contributing](#contributing) section below.

## Contributing

*Subsections:
[Commits and Pull Requests](#commits-and-pull-requests),
[Versioning](#versioning),
[Trello](#trello)*

### Commits and Pull Requests

A single commit should be a group of coherent changes that, together, serve a single purpose.

Commit messages should be concise and descriptive.

Use *imperatives* (Add, Change, Fix, etc.) instead of *past tense* (Added, Changed, Fixed, etc.).

Similarly, a Pull Request (PR) should have a singular purpose.

### Versioning

This project follows a versioning scheme, inspired by [semantic versioning](https://semver.org/).

Every version is labeled as `MAJOR.MINOR.PATCH`.
- `MAJOR`: Major changes or additions to game. Many players would have a different game experience after a change of this magnitude.
  
  This may include but is not limited to: complete rework of core game mechanics, introduction of an important new mechanic, etc.
  
- `MINOR`: Minor changes or additions to game. Most players would not feel that the game is significantly different.
  
  This may include but is not limited to: introduction of new QGB sets, adjustments to existing game mechanics, etc.
  
- `PATCH`: Bug fixes and/or polish. There is no mechanics change.

  This may include but is not limited to: bug fixes, new special effects, balance changes, performance optimizations, etc.

### Trello

The [trello board](https://trello.com/b/EyfOZSGU) tracks past, present, and future development progress of Spinball.

#### Labels

There are two primary types of labels: **Priority** labels and **Type** labels.

**Priority** labels start with `Priority`. They indicate the priority of a job (duh). 

There are currently 4 levels of priority: *Urgent*, *High*, *Medium*, and *Low*. Generally, high-priority cards would be rarer than low-priority cards.

Do **not** use the *Urgent* label lightly; it should only be applied to game-breaking bugs or other issues that must be addressed **immediately**.

**Type** labels start with `Major`, `Minor`, or `Patch`. They indicate the type of job the card represent.

The three prefixes of type labels correspond to the three parts of versioning scheme described in the [versioning](#versioning) section above. 

What comes after the prefix further refine the type of job: Aesthetics, Bug, Feature, etc.

#### Lists

There are several lists in the trello board.

- **Brainstorm**: Where we brainstorm new ideas. Developers may add their own ideas or transcribe ideas in user feature requests onto cards in this list.

- **To Do**: Ideas that are selected for development. Bug reports will also appear here.

- **Doing**: The card content is under development.

- **Done**: The card content is implemented and will be included in the next release.

- **Discarded Ideas**: Ideas that, for whatever reason, we decided not to do. This helps us keep track of discarded ideas for possible future use and to ensure consistency.

- **vx.x.x**: Cards that have been included in a previous release. This helps figuring out what changed in each release version.
