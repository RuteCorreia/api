const proxy_Host = 'https://localhost:7221/';
const proxyConfig = [
    {
        context: ['/api'],
        target: proxy_Host,
        secure: false
    },
];

module.exports = proxyConfig;