import { Table } from 'react-bootstrap';
import { createbookingsTable } from '../utils/bookingUtils';

import '../styles/App.css';

export const IdTokenData = (props) => {
    const tokenbookings = createbookingsTable(props.idTokenbookings);

    const tableRow = Object.keys(tokenbookings).map((key, index) => {
        return (
            <tr key={key}>
                {tokenbookings[key].map((claimItem) => (
                    <td key={claimItem}>{claimItem}</td>
                ))}
            </tr>
        );
    });
    
    return (
        <>
            <div className="data-area-div">
                <p>
                    See below the desk in our room.                  
                </p>
                <div className="data-area-div">
                    <Table responsive striped bordered hover>
                        <thead>
                            <tr>
                                <th>Claim</th>
                                <th>Value</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>{tableRow}</tbody>
                    </Table>
                    <Table responsive striped bordered hover>
                        <thead>
                            <tr>
                                <th>Claim</th>
                                <th>Value</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>{tableRow}</tbody>
                    </Table>
                </div>
            </div>
        </>
    );
};