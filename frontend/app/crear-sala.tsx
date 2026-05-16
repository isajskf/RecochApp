import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  SafeAreaView,
  Image,
  Dimensions,
  StatusBar,
  ScrollView,
} from 'react-native';

import { useState, useEffect } from 'react';

import { router } from 'expo-router';

import { LinearGradient } from 'expo-linear-gradient';

import * as ScreenOrientation from 'expo-screen-orientation';

const { width, height } = Dimensions.get('window');

export default function CrearSala() {

  useEffect(() => {
    ScreenOrientation.lockAsync(
      ScreenOrientation.OrientationLock.LANDSCAPE
    );
  }, []);

  const [modoSeleccionado, setModoSeleccionado] = useState('');

  const modos = [
    {
      id: 'TRIVIA',
      emoji: '🤔',
      color: '#ff2ec4',
      descripcion: 'Reta tus conocimientos',
      jugadores: '3-10 jugadores',
      intensidad: 'Media',
    },

    {
      id: 'RETOS',
      emoji: '🤣',
      color: '#1565c0',
      descripcion: 'Diversión pura',
      jugadores: '2-10 jugadores',
      intensidad: 'Media',
    },

    {
      id: 'PAREJA',
      emoji: '💕',
      color: '#8d2a84',
      descripcion: 'Romántico y atrevido',
      jugadores: '2 jugadores',
      intensidad: 'Alta',
    },

    {
      id: 'FIESTA',
      emoji: '🎉',
      color: '#9b59b6',
      descripcion: 'Perfecto para grupos',
      jugadores: '2-10 jugadores',
      intensidad: 'Media',
    },
  ];

  return (
    <>
      <StatusBar hidden />

      <LinearGradient
        colors={['#12001f', '#1d022e', '#12001f']}
        style={styles.container}
      >

        <SafeAreaView style={styles.safe}>

          <ScrollView
            contentContainerStyle={styles.scroll}
            showsVerticalScrollIndicator={false}
          >

            <View style={styles.card}>

              {/* LOGO */}
              <Image
                source={require('./assets/images/logo.png')}
                style={styles.logo}
              />

              {/* SUBTITULO */}
              <Text style={styles.subTitle}>
                - Modos de juego -
              </Text>

              {/* MODOS */}
              <View style={styles.modosContainer}>

                {modos.map((modo) => (

                  <TouchableOpacity
                    key={modo.id}
                    activeOpacity={0.85}
                    onPress={() => setModoSeleccionado(modo.id)}
                    style={[
                      styles.modoCard,
                      {
                        backgroundColor: modo.color,
                      },

                      modoSeleccionado === modo.id &&
                        styles.selectedCard,
                    ]}
                  >

                    <Text style={styles.emoji}>
                      {modo.emoji}
                    </Text>

                    <Text style={styles.cardTitle}>
                      Modo{'\n'}{modo.id}
                    </Text>

                    <Text style={styles.cardDescription}>
                      {modo.descripcion}
                    </Text>

                    <Text style={styles.cardInfo}>
                      {modo.jugadores}
                    </Text>

                    <Text style={styles.cardInfo}>
                      Intensidad: {modo.intensidad}
                    </Text>

                  </TouchableOpacity>

                ))}

              </View>

              {/* CREAR SALA */}
              <TouchableOpacity
                style={styles.startButton}
                onPress={() => router.push('/sala-espera')}
              >
                <Text style={styles.startText}>
                  + Crear sala
                </Text>
              </TouchableOpacity>

              {/* VOLVER */}
              <TouchableOpacity
                style={styles.backButton}
                onPress={() => router.back()}
              >
                <Text style={styles.backText}>
                  ← Volver al menú
                </Text>
              </TouchableOpacity>

            </View>

          </ScrollView>

        </SafeAreaView>

      </LinearGradient>
    </>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: '#12001f',
  },

  safe: {
    flex: 1,
  },

  scroll: {
    flexGrow: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingVertical: 20,
  },

  card: {
    width: '94%',
    minHeight: '88%',
    backgroundColor: '#220335',
    borderRadius: 30,
    alignItems: 'center',
    paddingTop: 15,
    paddingHorizontal: 15,
    paddingBottom: 25,
  },

  logo: {
    width: width * 0.22,
    height: 65,
    resizeMode: 'contain',
    marginBottom: 5,
  },

  subTitle: {
    color: '#fff',
    fontSize: 15,
    marginBottom: 15,
  },

  modosContainer: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 25,
  },

  modoCard: {
    width: '23%',
    minHeight: height * 0.42,
    borderRadius: 22,
    padding: 14,
    justifyContent: 'flex-start',
  },

  selectedCard: {
    borderWidth: 4,
    borderColor: '#fff',
  },

  emoji: {
    fontSize: 34,
    marginBottom: 10,
  },

  cardTitle: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 24,
    marginBottom: 10,
  },

  cardDescription: {
    color: '#fff',
    fontSize: 12,
    marginBottom: 15,
  },

  cardInfo: {
    color: '#fff',
    fontSize: 11,
    marginBottom: 8,
  },

  startButton: {
    backgroundColor: '#ff2ec4',
    width: 250,
    height: 55,
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 18,
  },

  startText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 20,
  },

  backButton: {
    backgroundColor: '#a93dc9',
    paddingHorizontal: 30,
    paddingVertical: 12,
    borderRadius: 20,
  },

  backText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 16,
  },

});