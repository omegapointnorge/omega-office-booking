export async function getUser() {
    const response = await fetch('client/user');
    console.log(response);
    return response.json();
}