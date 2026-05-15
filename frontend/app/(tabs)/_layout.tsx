import { Tabs } from 'expo-router';
import React from 'react';

export default function TabLayout() {
  return (
    <Tabs
      screenOptions={{
        headerShown: false,
        tabBarStyle: {
          backgroundColor: '#121212',
        },
        tabBarActiveTintColor: '#7c3aed',
        tabBarInactiveTintColor: '#888',
      }}>

      <Tabs.Screen
        name="parejas"
        options={{
          title: 'Parejas',
        }}
      />

      <Tabs.Screen
        name="fiesta"
        options={{
          title: 'Fiesta',
        }}
      />
    </Tabs>
  );
}