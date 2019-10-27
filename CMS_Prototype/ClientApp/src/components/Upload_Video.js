﻿import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import axios from 'axios';

class Upload_Video extends Component {


    constructor(props) {
        super(props);
        this.state = { video_file: '',video_url: '',selected_file: '',file_state: ''}
    }


    fileChangedHandler = event => {
        event.preventDefault();
        this.setState({ file_state: "loading" });
        let file = event.target.files[0];
        this.setState({
                selectedFile: file,
                file_state: "ready"
            });
        };

    uploadHandler = () => {
        let f = new FormData();
        f.append('File', this.state.selectedFile);
        axios.post('api/Videos', f,{
            headers: { 'Content-Type': 'multipart/form-data' }
        });
        alert("File upload completed");
    }


    render() {
        return (
            <div>
                <h1>Choose File {this.state.file_state}</h1>
                <p>
                    <input type="file" onChange={this.fileChangedHandler} />
                    <button onClick={this.uploadHandler}>Upload</button>
                </p>
            </div>
        );
    }
}



export default connect()(Upload_Video);
