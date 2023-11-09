export async function getUser() {
    
    try {
        const response = await fetch('client/user', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
        });
        
        return response.json();
    }catch(error){
        console.log(error);
    }
    
}