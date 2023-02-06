const PROXY_CONFIG =[
  {
    context: ['/api'],
    target:'https://localhost:5001/',
    secure:true,
    loglevel:'debug',
    //pathRewriter:{'^/api':'' }


  }
];

module.exports = PROXY_CONFIG;
