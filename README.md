# OpenRansom
Open Source Ransomware Client that connect to a server for randomly generated keys and ids. To set this up in your ransomware please read the README.MD for how to implement this into your ransomware.

<a rel="license" href="http://creativecommons.org/licenses/by/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by/4.0/">Creative Commons Attribution 4.0 International License</a>.


# USAGE

To get started add this class file into your project

2. add a namespace reference by adding using static OpenRansom.OpenRansom;
3. to make the ransomware connect to the server in the startup of your ransomware add "connecttoserver();"
4. to assign an id to the victim do "string id = getid();"
5. you can print this id whereever you want or put it on a label/textbox

Finally

if you want to check whether a key is valid or not use this example:

if(checkkey(id, key))
            {
                MessageBox.Show("Key is valid");
                //do something
            }
            else
            {
                MessageBox.Show("Key is not valid");
                //do something
            }
change the id and key to how you use it!
and then you're done!
