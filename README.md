The program emulates a old numpad behavior minus the user interface.

Given a character sequences resembling data entered in the old numpad, it converts thosse into its corresponding letter, if any.

Algorithm
    1. Given valid input.
    2. It calls ConvertNumpadInput.
        2.1. Then splits inside SplitInput
        2.2. Collect dictionary of delimeters (proof-of-concept)
        2.3. Processes each splitted group inside ProcessSegment
        2.4. Algo looks out for ending delimiters and removes it from final segment conversion. 
    3. Once all segments are analyzed it returns the final string.
    4. If any segment can't be converted, by default it will 
    print ?????

    Edge Case:
        A given input may end in *#, in which preceeding numeric sequences will be attempted to be converted (if any). In the event it doesn't, the algorithm removes the last/trailing 
        characters and reattempts again. It if doesn't find any, final result will be ?????
Testing.

    Install dotnet sdk & runtime prior.
    Proceed to test folder directory; there enter following commands

    dotnet restore
    dotnet test

Limitations.

    1. Case-insensitivity and lack of special character selections. 
    2. Lack of multi-mode intrepreting the sequence, with #1 as a symptom.
    3. It can handle an input containing homogenous patterns. In other words, a single input may contain 
       a mix of delimiters (*, 0, whitespace).
    4. Algo expects # on the end. Any preceding # would give different results.


Future considerations.

    1. Multi-mode. Enable cha
    2. File I/O; requires more asynchronous handling and dynamic memory allocation to account varying filesize.
    3. Create U/I. For the nostalgia and enabling learning to younger generations who weren't exposed.
    4. Predictive text. A huge undertaking, likely after 3. Can supercede 3 for text analysis cases.
    5. Efficient backtracking or more robust use of 2-pointer algo in string. When *# edge case arises, a flag has to be set to false and reset the input.
        5.1. Process segment itself would iterate the whole string input.
    6. Improved delimiter pattern in separating string input. Current algorithm separates * and #, making a more exhaustie process.
    7. Scanning each group segment if it is T9 or condensed pattern would help parsing input better. 