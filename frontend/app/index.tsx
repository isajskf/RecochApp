import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  Image,
} from 'react-native';

import { router } from 'expo-router';
import { LinearGradient } from 'expo-linear-gradient';

import * as ScreenOrientation from 'expo-screen-orientation';
import { useEffect } from 'react';

export default function Home() {

  useEffect(() => {
    ScreenOrientation.lockAsync(
      ScreenOrientation.OrientationLock.LANDSCAPE
    );
  }, []);

  return (
    <LinearGradient
      colors={['#1d022e', '#25053a', '#2d0b45']}
      style={styles.container}
    >

      {/* CARD */}
      <View style={styles.card}>

        {/* LOGO */}
        <Image
          source={require('./assets/images/logo.png')}
          style={styles.logoImage}
        />

        {/* BOTONES */}
        <View style={styles.menuRow}>

          {/* CREAR */}
          <TouchableOpacity
            style={styles.menuButtonLeft}
            onPress={() => router.push('/crear-sala')}
          >
            <Text style={styles.menuTitle}>
              Crear una sala
            </Text>

            <Text style={styles.icon}>🎮</Text>
          </TouchableOpacity>

          {/* UNIRSE */}
          <TouchableOpacity
            style={styles.menuButtonRight}
            onPress={() => router.push('/unirse-sala')}
          >
            <Text style={styles.menuTitle}>
              Unirse a una sala
            </Text>

            <Text style={styles.icon}>🖱️</Text>
          </TouchableOpacity>

        </View>

        {/* BOTON */}
        <TouchableOpacity style={styles.logoutButton}>
          <Text style={styles.logoutText}>
            Cerrar sesión
          </Text>
        </TouchableOpacity>

      </View>

    </LinearGradient>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: '#1d022e',
    justifyContent: 'center',
    alignItems: 'center',
  },

  card: {
    width: '92%',
    backgroundColor: '#220335',
    borderRadius: 35,
    paddingVertical: 30,
    paddingHorizontal: 20,
    alignItems: 'center',
  },

  logoImage: {
    width: 260,
    height: 90,
    resizeMode: 'contain',
    marginBottom: 30,
  },

  menuRow: {
    flexDirection: 'row',
    justifyContent: 'center',
    gap: 20,
    marginBottom: 30,
  },

  menuButtonLeft: {
    width: 150,
    height: 120,
    backgroundColor: '#8d2a84',
    borderRadius: 25,
    justifyContent: 'center',
    alignItems: 'center',
  },

  menuButtonRight: {
    width: 150,
    height: 120,
    backgroundColor: '#ff2ec4',
    borderRadius: 25,
    justifyContent: 'center',
    alignItems: 'center',
  },

  menuTitle: {
    color: '#fff',
    fontSize: 15,
    fontWeight: 'bold',
    textAlign: 'center',
    marginBottom: 10,
  },

  icon: {
    fontSize: 45,
  },

  logoutButton: {
    width: 220,
    backgroundColor: '#7f3b87',
    paddingVertical: 14,
    borderRadius: 15,
    alignItems: 'center',
  },

  logoutText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },

});