import React, { useCallback, Component } from 'react';
import { debounce, uniqueId } from 'lodash';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { files: [], loading: true, search: '' };
        this.handler = debounce(this.fetchData, 3000);
    }

    static renderFiles(files) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Filename</th>
                        <th>Path</th>
                        <th>Count</th>
                    </tr>
                </thead>
                <tbody>
                    {files.map(file =>
                        <tr key={uniqueId()}>
                            <td>{file.name}</td>
                            <td>{file.path}</td>
                            <td>{file.count}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderFiles(this.state.files);

        return (
            <div>
                <h1 id="tabelLabel" >File text search:</h1>
                <input type="text" onChange={this.handleChange} />
                {contents}
            </div>
        );
    }

    fetchData(value) {
        (async () => {
            const response = await fetch('api/Files/' + value);
            const data = await response.json();

            this.setState({ loading: false, files: data });
        })();
    }

    handleChange = event => {
        try {
            if (event.target.value.length > 2) {
                this.setState({ loading: true });
                this.handler(event.target.value);
            }
        } catch (e) { }
    };


}
