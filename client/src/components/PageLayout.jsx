

import { NavigationBar } from './NavigationBar.jsx';

export const PageLayout = (props) => {
    /**
     * Most applications will need to conditionally render certain components based on whether a user is signed in or not.
     * msal-react provides 2 easy ways to do this. AuthenticatedTemplate and UnauthenticatedTemplate components will
     * only render their children if a user is authenticated or unauthenticated, respectively.
     */
    return (
        <>
            <br />
            <h5>
                <center>Hei! Welcome to Room page</center>
            </h5>
            <br />
            <br />
        </>
    );
}
export default PageLayout;