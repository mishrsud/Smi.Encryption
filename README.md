# README #

This repository contains the code for a tool that can generate cryptographically secure random numbers that can be used for basic auth key/secret pairs.
It can also do Windows machine key encryption/decryption.

### Running the tool ###

When built, you should see a Tools directory at the same level as the solution file. Inside the Tools -> Command line directory, open a command prompt (as an administrator), then run the command line as follows:

```bash
enctool cred
```

This will generate a set of credentials and also echo back the basic auth header:

```bash
=========================================================================
API Key   : abbc324843274cf69fb0fae33b8382a4
API Secret: AeWpmMq8kByuvRuxDz/DQxpvh59oj/eIG/yODy+i81s=

=========================================================================

=========================================================================
Authorization header:
Basic YWJiYzMyNDg0MzI3NGNmNjlmYjBmYWUzM2I4MzgyYTQ6QWVXcG1NcThrQnl1dlJ1eER6L0RReHB2aDU5b2ovZUlHL3lPRHkraTgxcz0=
=========================================================================
```

### Motivation and Inspiration ###

Inspired in parts by this post from Jon Galloway: http://weblogs.asp.net/jongalloway/encrypting-passwords-in-a-net-app-config-file
