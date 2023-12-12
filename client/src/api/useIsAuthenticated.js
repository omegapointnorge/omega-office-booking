import { useQuery } from '@tanstack/react-query';

const url = `/api/Account/IsAuthenticated`;

export const getIsAuthenticated = async () => {
    const requestOptions = {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
    };
    
    const isAuthenticated = await fetch('/api/Account/IsAuthenticated', 
        requestOptions);
    
    if(isAuthenticated.ok || isAuthenticated.status === 200){
      return true;
    }
    
    return Promise.reject(new Error('Could not authenticate user'));
}

export const useIsAuthenticated = () => {
    return useQuery({
        queryKey: ['isAuthenticated'],
        queryFn: async () => await getIsAuthenticated(),
        retry: false
    });
};