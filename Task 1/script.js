let [m='', ...a] = process.argv.slice(2), n = m.length;
for (let i = n; i > 0; i--) 
    for (let j = 0; j <= n - i; j++) { 
        let s = m.substr(j, i);
        if (a.every(x => x.includes(s))) { 
            console.log(s); 
            process.exit(); 
        } 
    } 
console.log('');