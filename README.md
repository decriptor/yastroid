It would appear that WebYaST is dead and so I'll no longer waste time on this project.






YaSTroid project
    An android application that will communicate with webyast
    service through it's RESTful interface.


Currently the RESTful interface only listens on localhost.  This 
presents an obvious problem, so in order to change this you will 
have to make some changes.

    vi /etc/yastws/lighttpd.conf
    server.bind = "0.0.0.0" (change it to this)
    rcyastws restart

And you should be ready to start testing the RESTful interface.

*** If you want to test the RESTful interface from the console
To generate the cookie:
    curl -u root -c cookie.txt http://serverip:4984/login

Once you have run that you can run this to get all available Yast resources
    curl -0 -X GET -b cookie.txt http://serverip:4984/resources

For example
    curl -0 -X GET -b cookie.txt http://serverip:4984/ntp


NOTE:
	I've started a new job where I'm developing on android using mono for android.
	So, as a way to improve my skills I'm porting yastroid over to m4a.


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/decriptor/yastroid/trend.png)](https://bitdeli.com/free "Bitdeli Badge")
