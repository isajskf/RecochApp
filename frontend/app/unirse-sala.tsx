import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  SafeAreaView,
  Image,
  StatusBar,
  Dimensions,
  TextInput,
} from 'react-native';

import { useEffect, useState, useRef } from 'react';

import { router } from 'expo-router';

import * as ScreenOrientation from 'expo-screen-orientation';

const { width, height } = Dimensions.get('window');

export default function UnirseSala() {

  useEffect(() => {
    ScreenOrientation.lockAsync(
      ScreenOrientation.OrientationLock.LANDSCAPE
    );
  }, []);

  const [codigo, setCodigo] = useState(['', '', '', '', '']);

  const inputs = useRef<TextInput[]>([]);

  const cambiarNumero = (text: string, index: number) => {

    if (!/^[0-9]?$/.test(text)) return;

    const nuevoCodigo = [...codigo];
    nuevoCodigo[index] = text;

    setCodigo(nuevoCodigo);

    // PASAR AL SIGUIENTE INPUT
    if (text && index < 4) {
      inputs.current[index + 1]?.focus();
    }
  };

  return (
    <>
      <StatusBar hidden />

      <SafeAreaView style={styles.container}>

        {/* CONTENIDO */}
        <View style={styles.card}>

          {/* LOGO */}
          <Image
            source={require('./assets/images/logo.png')}
            style={styles.logo}
          />

          {/* TITULO */}
          <Text style={styles.title}>
            Unirse a una sala
          </Text>

          {/* SUBTITULO */}
          <Text style={styles.subtitle}>
            Ingresa el código y accede a tu partida
          </Text>

          {/* INPUTS */}
          <View style={styles.inputsRow}>

            {codigo.map((numero, index) => (

              <TextInput
                key={index}
                ref={(ref) => {
                  if (ref) {
                    inputs.current[index] = ref;
                  }
                }}
                style={styles.input}
                value={numero}
                onChangeText={(text) =>
                  cambiarNumero(text, index)
                }
                keyboardType="numeric"
                maxLength={1}
                placeholder="0"
                placeholderTextColor="#f3c7ff"
                caretHidden={true}
              />

            ))}

          </View>

          {/* BOTONES */}
          <View style={styles.buttonsRow}>

            {/* BOTON UNIRME */}
            <TouchableOpacity
              style={styles.joinButton}
              onPress={() => router.push('/sala-espera')}
            >

              <Text style={styles.joinText}>
                Unirme
              </Text>

            </TouchableOpacity>

            {/* BOTON VOLVER */}
            <TouchableOpacity
              style={styles.backButton}
              onPress={() => router.back()}
            >

              <Text style={styles.backText}>
                Volver al menú
              </Text>

            </TouchableOpacity>

          </View>

        </View>

      </SafeAreaView>
    </>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: '#220335',
    justifyContent: 'center',
    alignItems: 'center',
  },

  card: {
    width: '95%',
    height: '88%',
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingHorizontal: width * 0.03,
    paddingTop: height * 0.12,
  },

  logo: {
    width: width * 0.27,
    height: height * 0.17,
    resizeMode: 'contain',
    marginTop: -height * 0.14,
    marginBottom: height * 0.015,
  },

  title: {
    color: '#fff',
    fontSize: width * 0.026,
    fontWeight: 'bold',
    marginBottom: 8,
  },

  subtitle: {
    color: '#fff',
    fontSize: width * 0.015,
    marginBottom: height * 0.07,
    textAlign: 'center',
  },

  inputsRow: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: height * 0.08,
  },

  input: {
    width: width * 0.105,
    height: height * 0.12,
    backgroundColor: '#d315b6',
    borderRadius: 18,
    marginHorizontal: width * 0.015,

    color: '#fff',
    fontSize: width * 0.04,
    fontWeight: 'bold',

    textAlign: 'center',
    textAlignVertical: 'center',

    padding: 0,
    paddingBottom: 2,

    borderWidth: 2,
    borderColor: '#ff4dd2',

    shadowColor: '#ff4dd2',
    shadowOffset: {
      width: 0,
      height: 4,
    },

    shadowOpacity: 0.4,
    shadowRadius: 8,
    elevation: 8,
  },

  buttonsRow: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    gap: width * 0.025,
  },

  joinButton: {
    width: width * 0.20,
    height: height * 0.08,
    backgroundColor: '#c218b9',
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
  },

  joinText: {
    color: '#fff',
    fontSize: width * 0.02,
    fontWeight: 'bold',
  },

  backButton: {
    width: width * 0.20,
    height: height * 0.08,
    backgroundColor: '#8e24aa',
    borderRadius: 18,
    justifyContent: 'center',
    alignItems: 'center',
  },

  backText: {
    color: '#fff',
    fontSize: width * 0.018,
    fontWeight: 'bold',
  },

});