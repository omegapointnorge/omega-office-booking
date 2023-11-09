export async function getUser() {
    const response = await fetch('client/user', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
        },
    });
    return response.json();
}