const mongoose = require('mongoose');
const Account = mongoose.model('accounts');

const argon2i = require('argon2-ffi').argon2i;
const crypto = require('crypto');

const passwordRegex = new RegExp("((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,30})");
//((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})
//console.log(passwordRegex);


module.exports = app => {
    //Routes
    app.post('/account/login', async (req, res) => {
        //console.log(req.body.rUsername);

        var response = {}

        const { rUsername, rPassword } = req.body;
        if (rUsername == null || !passwordRegex.test(rPassword)) {
            response.code = 1;
            response.message = "Invalid credentials"
            res.send(response);
            return;
        }

        var userAccount = await Account.findOne({ username: rUsername },"username adminFlag password");
        //console.log(userAccount);
        if (userAccount != null) {
            argon2i.verify(userAccount.password, rPassword).then(async(success) => {
                if (success) {
                    userAccount.lastAuthentication = Date.now();
                    await userAccount.save();

                    response.code = 0;
                    response.message = "Account found"
                    response.data = ( ({username,adminFlag}) => ({username,adminFlag}) ) (userAccount);
                    res.send(response);

                    return;

                }
                else{
                    response.code = 1;
                    response.message = "Invalid credentials"
                    res.send(response);
                    return;
                }
            }); 
        }
        else{
            response.code = 1;
            response.message = "Invalid credentials"
            res.send(response);
            return;
        }
    });


//------------------------------------------------------------------------------------------------
    app.post('/account/create', async (req, res) => {
        //console.log(req.body.rUsername);
        var response = {}

        const { rUsername, rPassword } = req.body;
        if (rUsername == null || rPassword.length < 3 || rPassword.length > 30) {
            response.code = 1;
            response.message = "Invalid credentials"
            res.send(response);
            return;
        }

        if(!passwordRegex.test(rPassword)){
            response.code = 3;
            response.message = "Unsafe password"
            res.send(response);
            return;
        }

        var userAccount = await Account.findOne({ username: rUsername },"_id");
        //console.log(userAccount);
        if (userAccount == null) {
            //create a new account
            console.log('Creating new account');

            //Generate a unique access token

            crypto.randomBytes(32, function(err,salt){
                if(err){
                    console.log(err);
                }
                argon2i.hash(rPassword,salt).then(async(hash) => {
                    var newAccount = new Account({
                        username: rUsername,
                        password: hash,
                        salt: salt,
        
                        lastAuthentication: Date.now(),
                    });
                    await newAccount.save();

                    response.code = 0;
                    response.message = "Account found"
                    response.data = ( ({username}) => ({username}) ) (newAccount);
                    res.send(response);
                    return;
                });
            });

        } else {
            response.code = 2;
            response.message = "Username already in use"
            res.send(response);
        }
        
        return;
    });
}