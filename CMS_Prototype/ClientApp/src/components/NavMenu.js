import React from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';
import { connect } from 'react-redux';

class NavMenu extends React.Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.state = {
            isOpen: false
        };
    }
    toggle() {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }
    render() {
        return (
            <Navbar inverse fixedTop fluid collapseOnSelect>
                <Navbar.Header>
                    <Navbar.Brand>
                        <Link to={'/'}>CMS_Prototype</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle />
                </Navbar.Header>
                <Navbar.Collapse>
                    {this.props.user.isAuthenticated &&
                        <Nav>
                            <LinkContainer to={'/upload_video'}>
                                <NavItem>
                                     Video
                            </NavItem>
                            </LinkContainer>
                            <LinkContainer to={'/all_videos'}>
                                <NavItem>
                                    <Glyphicon glyph='th-list' /> All Videos
                         </NavItem>
                            </LinkContainer>
                        </Nav>
                    }
                    {!this.props.user.isAuthenticated &&
                        <Nav>
                            <LinkContainer to={'/login'}>
                                <NavItem>
                                <Glyphicon /> Login
                            </NavItem>
                            </LinkContainer>
                        </Nav>
                    }
                </Navbar.Collapse>
            </Navbar>
        );
    }
}

function mapStateToProps(state) {
    return { user: state.user }
}

export default connect(mapStateToProps)(NavMenu);