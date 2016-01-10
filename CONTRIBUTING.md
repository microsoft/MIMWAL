## Contributing to MIMWAL Code

The two ways to contribute for MIMWAL Code are: Fork the Repository or Submit an Issue.

### Fork the MIMWAL Repository.

Fork a copy of the MIM Repo, make the changes in your fork and send a pull request. Forking a repository allows you to freely experiment with changes without affecting the original project. See [Fork A Repo](https://help.github.com/articles/fork-a-repo/) help article for more information.

#### Coding Guidelines

When developing the code, make sure that you instrument your code adequately. And what's better way to know whether it's adequate or not that than not using the debugger in the first place!! Not installing Visual Studio on your MIM server in a great way to avoid temptation to use debugger. 

As for the coding style, it's simple. Download and install [StyleCop](http://stylecop.codeplex.com/) and let it guide you. Note: At the time of writing, StyleCop does not support Visual Studio 2015 so you'll need to use Visual Studio 2013.

##### Code Acceptance

While code acceptance for a bug fix or a minor enhancement like adding a new function is likely to be very straightforward, the code for a major new feature such as a new activity will only be accepted only if there is consensus among ALL project admins. Any proposal for a new feature that requires extension of MIM schema will be rejected. Any proposal that has serious backward compatibility considerations is unlikely to get accepted. A classic example of this is that contrary to specification of FIM string functions such as `Mid`, all WAL string functions expect a zero-based index.

### Submit an Issue.

You can also submit an issue. Use the [Issues](https://github.com/Microsoft/MIMWAL/issues) tab to create a new issue.


## Contributing to MIMWAL Wiki

The two ways to contribute for MIMWAL Wiki are again: Fork the Repository or Submit an Issue.

### Fork the MIMWAL Wiki Repository.

Fork a copy of the [MIMWAL Wiki Repo](https://github.com/Microsoft/MIMWAL/wiki) and send a pull request.

### Submit an Issue.

You can also submit an issue. Use the [Issues](https://github.com/Microsoft/MIMWAL/issues) tab to create a new issue.

##### Wiki Acceptance

Your edits will be reviewed and accepted if they align with the recommended practices / product architecture and not in conflict with prior lessons learnt by the community. Any other edits may still be accepted with proper caveats or special considerations called out appropriately.
