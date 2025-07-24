The program emulates a old numpad behavior minus the user interface.

Given a character sequences resembling data entered in the old numpad, it converts thosse into its corresponding letter, if any.

Testing.

    Install dotnet sdk & runtime prior.
    Proceed to test folder directory; there enter following commands

    dotnet restore
    dotnet test

Limitations.

    1. Case-insensitivity. 
    2. Lack of multi-mode intrepreting the sequence, with #1 as a symptom.


Future considerations.

    1. Multi-mode. Enable cha
    2. File I/O; requires more asynchronous handling and dynamic memory allocation to account varying filesize.
    3. Create U/I. For the nostalgia and enabling learning to younger generations who weren't exposed.
    4. Predictive text. A huge undertaking, likely after 3. Can supercede 3 for text analysis cases.