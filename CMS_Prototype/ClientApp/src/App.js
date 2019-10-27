import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Upload_Video from './components/Upload_Video';
import Show_Video from './components/Show_Video';
import Login from './components/Login';
import Videos from './components/Videos';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/login' component={Login} />
    <Route path='/upload_video' component={Upload_Video} />
    <Route path='/show_video/:id' component={Show_Video} />
    <Route path='/all_videos' component={Videos} />
  </Layout>
);
